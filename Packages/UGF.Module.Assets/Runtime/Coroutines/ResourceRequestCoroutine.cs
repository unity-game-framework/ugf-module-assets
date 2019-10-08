using System.Collections;
using UGF.Coroutines.Runtime;
using UnityEngine;

namespace UGF.Module.Assets.Runtime.Coroutines
{
    public class ResourceRequestCoroutine<TAsset> : Coroutine<TAsset>
    {
        public ResourceRequest Request { get; }

        public ResourceRequestCoroutine(ResourceRequest request)
        {
            Request = request;
        }

        protected override IEnumerator Routine()
        {
            yield return Request;

            if (Request.asset is TAsset asset)
            {
                Result = asset;
            }
        }
    }
}
