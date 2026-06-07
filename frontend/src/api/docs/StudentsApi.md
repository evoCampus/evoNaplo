# StudentsApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiStudentsGet**](#apistudentsget) | **GET** /api/Students | |
|[**apiStudentsIdDelete**](#apistudentsiddelete) | **DELETE** /api/Students/{id} | |
|[**apiStudentsIdGet**](#apistudentsidget) | **GET** /api/Students/{id} | |
|[**apiStudentsIdPut**](#apistudentsidput) | **PUT** /api/Students/{id} | |
|[**apiStudentsPost**](#apistudentspost) | **POST** /api/Students | |

# **apiStudentsGet**
> Array<StudentDTO> apiStudentsGet()


### Example

```typescript
import {
    StudentsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new StudentsApi(configuration);

const { status, data } = await apiInstance.apiStudentsGet();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**Array<StudentDTO>**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentsIdDelete**
> apiStudentsIdDelete()


### Example

```typescript
import {
    StudentsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new StudentsApi(configuration);

let id: string; // (default to undefined)

const { status, data } = await apiInstance.apiStudentsIdDelete(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**string**] |  | defaults to undefined|


### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentsIdGet**
> StudentDTO apiStudentsIdGet()


### Example

```typescript
import {
    StudentsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new StudentsApi(configuration);

let id: string; // (default to undefined)

const { status, data } = await apiInstance.apiStudentsIdGet(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**string**] |  | defaults to undefined|


### Return type

**StudentDTO**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentsIdPut**
> apiStudentsIdPut()


### Example

```typescript
import {
    StudentsApi,
    Configuration,
    StudentDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new StudentsApi(configuration);

let id: string; // (default to undefined)
let studentDTO: StudentDTO; // (optional)

const { status, data } = await apiInstance.apiStudentsIdPut(
    id,
    studentDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **studentDTO** | **StudentDTO**|  | |
| **id** | [**string**] |  | defaults to undefined|


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

# **apiStudentsPost**
> StudentDTO apiStudentsPost()


### Example

```typescript
import {
    StudentsApi,
    Configuration,
    StudentDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new StudentsApi(configuration);

let studentDTO: StudentDTO; // (optional)

const { status, data } = await apiInstance.apiStudentsPost(
    studentDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **studentDTO** | **StudentDTO**|  | |


### Return type

**StudentDTO**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

