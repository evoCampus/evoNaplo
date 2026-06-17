# AuthApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiAuthLoginPost**](#apiauthloginpost) | **POST** /api/Auth/login | |
|[**apiAuthRegisterPost**](#apiauthregisterpost) | **POST** /api/Auth/register | |

# **apiAuthLoginPost**
> apiAuthLoginPost()


### Example

```typescript
import {
    AuthApi,
    Configuration,
    LoginDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthApi(configuration);

let loginDTO: LoginDTO; // (optional)

const { status, data } = await apiInstance.apiAuthLoginPost(
    loginDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **loginDTO** | **LoginDTO**|  | |


### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAuthRegisterPost**
> apiAuthRegisterPost()


### Example

```typescript
import {
    AuthApi,
    Configuration,
    RegisterDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new AuthApi(configuration);

let registerDTO: RegisterDTO; // (optional)

const { status, data } = await apiInstance.apiAuthRegisterPost(
    registerDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **registerDTO** | **RegisterDTO**|  | |


### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

