using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Experiment.NoobAutoLinker.Core;
using UnityEditor;
using UnityEngine;
using UnityUtils;
using Object = UnityEngine.Object;

namespace Experiment.NoobAutoLinker.Editor
{
    public enum LinkResultType
    {
        Success,
        MoreThanOneInstance,
        NoExistingObject
    }

    public struct LinkResult
    {
        public LinkResult(Object objectToLink, LinkResultType resultType, string details, FieldInfo fieldInfo)
        {
            ObjectToLink = objectToLink;
            ResultType = resultType;
            Details = details;
            FieldInfo = fieldInfo;
        }

        public Object ObjectToLink { get; }
        public LinkResultType ResultType { get; }
        public string Details { get; }
        public FieldInfo FieldInfo { get; }
    }

    public class AutoLinker
    {
        private readonly IDictionary<Type, ISet<Object>> _buckets;

        private string[] _directories;

        public AutoLinker(string[] directories)
        {
            _directories = directories;
            _buckets = new Dictionary<Type, ISet<Object>>();

            InitTypeBuckets();
        }

        public void Refresh()
        {
            InitTypeBuckets();
        }

        public void SetDirectories(string[] directories)
        {
            _directories = directories;

            Refresh();
        }

        public IEnumerable<LinkResult> InjectToObject(Object objectToLink, bool shouldOverrideExistingValue)
        {
            var fieldsForAutoLink = ReflectionUtils.GetFieldsByAttribute(objectToLink.GetType(), typeof(AutoLinkAttribute)).ToArray();

            foreach (var fieldInfo in fieldsForAutoLink)
                if (_buckets.TryGetValue(fieldInfo.FieldType, out var possibleValues))
                {
                    if (!possibleValues.Any())
                        yield return HandleNoExistObjectWithRequiredType(objectToLink, fieldInfo, fieldInfo.FieldType);
                    else if (possibleValues.Count > 1)
                        yield return HandleMoreThanOneOptions(objectToLink, fieldInfo, possibleValues);
                    else if (fieldInfo.GetValue(objectToLink) == null || shouldOverrideExistingValue)
                        yield return HandleSuccessfulInjection(objectToLink, fieldInfo, possibleValues.First());
                }
                else
                {
                    yield return HandleNoExistObjectWithRequiredType(objectToLink, fieldInfo, fieldInfo.FieldType);
                }
        }

        public IEnumerable<LinkResult> InjectToAllScriptableWithinSearchDirectory(bool shouldOverrideExistingValue)
        {
            return GetAllScriptableWithinSearchDirectory(_directories)
                .SelectMany(scriptableObject => InjectToObject(scriptableObject, shouldOverrideExistingValue));
        }

        private static LinkResult HandleSuccessfulInjection(Object objectToLink, FieldInfo fieldInfo, Object value)
        {
            fieldInfo.SetValue(objectToLink, value);

            return new LinkResult(objectToLink, LinkResultType.Success, $"Linked {objectToLink.name} with {value.name}", fieldInfo);
        }


        private LinkResult HandleNoExistObjectWithRequiredType(Object objectToLink, FieldInfo fieldInfo, Type requiredType) =>
            new LinkResult(
                objectToLink,
                LinkResultType.NoExistingObject,
                $"There is no existing scriptable within ({string.Join(",", _directories)}) that is of type {requiredType}",
                fieldInfo
            );

        private LinkResult HandleMoreThanOneOptions(Object objectToLink, FieldInfo fieldInfo, IEnumerable<Object> possibleValues)
        {
            return new LinkResult(
                objectToLink,
                LinkResultType.MoreThanOneInstance,
                $"There are more than one option to link {objectToLink.name} within ({string.Join(",", _directories)}), including {string.Join(",", possibleValues.Select(o => o.name))}",
                fieldInfo
            );
        }

        private void InitTypeBuckets()
        {
            _buckets.Clear();
            var assets = GetAllScriptableWithinSearchDirectory(_directories);

            foreach (var asset in assets)
            {
                var assetType = asset.GetType();

                if (_buckets.TryGetValue(assetType, out var bucket))
                {
                    bucket.Add(asset);
                }
                else
                {
                    var newBucket = new HashSet<Object> {asset};
                    _buckets[assetType] = newBucket;
                }
            }
        }

        private static IEnumerable<ScriptableObject> GetAllScriptableWithinSearchDirectory(string[] directories) =>
            AssetDatabase.FindAssets("t:ScriptableObject", directories).Select(GetAssetFromGuid);

        private static ScriptableObject GetAssetFromGuid(string guid)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            return asset;
        }
    }
}