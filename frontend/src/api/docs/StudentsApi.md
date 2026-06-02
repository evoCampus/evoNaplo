# StudentsApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiStudentsGet**](#apistudentsget) | **GET** /api/Students | |
|[**apiStudentsPost**](#apistudentspost) | **POST** /api/Students | |
|[**apiStudentsStudentIdDelete**](#apistudentsstudentiddelete) | **DELETE** /api/Students/{studentId} | |
|[**apiStudentsStudentIdGet**](#apistudentsstudentidget) | **GET** /api/Students/{studentId} | |
|[**apiStudentsStudentIdPut**](#apistudentsstudentidput) | **PUT** /api/Students/{studentId} | |

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

# **apiStudentsStudentIdDelete**
> apiStudentsStudentIdDelete()


### Example

```typescript
import {
    StudentsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new StudentsApi(configuration);

let studentId: string; // (default to undefined)

const { status, data } = await apiInstance.apiStudentsStudentIdDelete(
    studentId
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **studentId** | [**string**] |  | defaults to undefined|


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

# **apiStudentsStudentIdGet**
> StudentDTO apiStudentsStudentIdGet()


### Example

```typescript
import {
    StudentsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new StudentsApi(configuration);

let studentId: string; // (default to undefined)

const { status, data } = await apiInstance.apiStudentsStudentIdGet(
    studentId
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **studentId** | [**string**] |  | defaults to undefined|


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

# **apiStudentsStudentIdPut**
> apiStudentsStudentIdPut()


### Example

```typescript
import {
    StudentsApi,
    Configuration,
    StudentDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new StudentsApi(configuration);

let studentId: string; // (default to undefined)
let studentDTO: StudentDTO; // (optional)

const { status, data } = await apiInstance.apiStudentsStudentIdPut(
    studentId,
    studentDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **studentDTO** | **StudentDTO**|  | |
| **studentId** | [**string**] |  | defaults to undefined|


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

