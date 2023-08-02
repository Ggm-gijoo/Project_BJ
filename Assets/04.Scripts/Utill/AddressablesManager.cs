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
	/// ��巹���� ���ҽ� ������ ����ϴ� ��ƿ ��ũ��Ʈ
	/// </summary>
	public class AddressablesManager : MonoSingleton<AddressablesManager>
	{
		/// <summary>
		/// ���ҽ��� �������� �Լ�
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
		/// �񵿱�� ���ҽ��� �������� �Լ�, �ڵ��� �Ϸ�ɽÿ� �ݹ� �Լ��� �߰��ؾ���
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
		/// �񵿱�� ���ҽ��� �������� �Լ�, ������ �Լ��� ����� �־������
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
		/// �񵿱�� ���ҽ��� �������� �Լ�, ������ �Լ��� ����� �־������
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