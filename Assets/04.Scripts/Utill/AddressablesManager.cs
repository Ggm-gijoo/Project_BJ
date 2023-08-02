using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utill.Pattern;

namespace Utill.Addressable
{
	/// <summary>
	/// 어드레서블 리소스 전반을 담당하는 유틸 스크립트
	/// </summary>
	public class AddressablesManager : MonoSingleton<AddressablesManager>
	{
		/// <summary>
		/// 리소스를 가져오는 함수
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="_name"></param>
		/// <returns></returns>
		public T GetResource<T>(string _name)
		{
			var _handle = Addressables.LoadAssetAsync<T>(_name);

    		_handle.WaitForCompletion();

			return _handle.Result;
		}

		/// <summary>
		/// 비동기로 리소스를 가져오는 함수, 핸들이 완료될시에 콜백 함수를 추가해야함
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="_name"></param>
		/// <param name="_action"></param>
		public AsyncOperationHandle<T> GetResourceAsync<T>(string _name)
		{
			var _handle = Addressables.LoadAssetAsync<T>(_name);
			
			return _handle;
		}
		
		
		public AsyncOperationHandle<T> GetResourceThread<T>(string _name)
		{
			var _handle = Addressables.LoadAssetAsync<T>(_name);
			
			return _handle;
		}

		/// <summary>
		/// 비동기로 리소스를 가져오는 함수, 별도의 함수를 만들고 넣어줘야함
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="_name"></param>
		/// <param name="_action"></param>
		public AsyncOperationHandle<T> GetResourceAsync<T>(string _name, System.Action<T> _action)
		{
			var _handle = Addressables.LoadAssetAsync<T>(_name);
			_handle.Completed += (_x) =>
			{
				_action(_x.Result);
			};
			return _handle;
		}

		/// <summary>
		/// 비동기로 리소스를 가져오는 함수, 별도의 함수를 만들고 넣어줘야함
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <param name="_action"></param>
		public AsyncOperationHandle<T> GetResourceAsync<T, J>(string name, System.Action<T, J> _action, J _parameter)
		{
			var _handle = Addressables.LoadAssetAsync<T>(name);
			_handle.Completed += (_x) =>
			{
				_action(_x.Result, _parameter);
			};
			return _handle;
		}
	}

}