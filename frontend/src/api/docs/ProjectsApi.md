# ProjectsApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiProjectsPost**](#apiprojectspost) | **POST** /api/Projects | |
|[**apiProjectsProjectIdDelete**](#apiprojectsprojectiddelete) | **DELETE** /api/Projects/{projectId} | |
|[**apiProjectsProjectIdPut**](#apiprojectsprojectidput) | **PUT** /api/Projects/{projectId} | |
|[**getProject**](#getproject) | **GET** /api/Projects/{projectId} | |
|[**getProjects**](#getprojects) | **GET** /api/Projects | |

# **apiProjectsPost**
> ProjectDTO apiProjectsPost()


### Example

```typescript
import {
    ProjectsApi,
    Configuration,
    ProjectDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new ProjectsApi(configuration);

let projectDTO: ProjectDTO; // (optional)

const { status, data } = await apiInstance.apiProjectsPost(
    projectDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **projectDTO** | **ProjectDTO**|  | |


### Return type

**ProjectDTO**

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

# **apiProjectsProjectIdDelete**
> apiProjectsProjectIdDelete()


### Example

```typescript
import {
    ProjectsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new ProjectsApi(configuration);

let projectId: string; // (default to undefined)

const { status, data } = await apiInstance.apiProjectsProjectIdDelete(
    projectId
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **projectId** | [**string**] |  | defaults to undefined|


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

# **apiProjectsProjectIdPut**
> apiProjectsProjectIdPut()


### Example

```typescript
import {
    ProjectsApi,
    Configuration,
    ProjectDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new ProjectsApi(configuration);

let projectId: string; // (default to undefined)
let projectDTO: ProjectDTO; // (optional)

const { status, data } = await apiInstance.apiProjectsProjectIdPut(
    projectId,
    projectDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **projectDTO** | **ProjectDTO**|  | |
| **projectId** | [**string**] |  | defaults to undefined|


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

# **getProject**
> ProjectDTO getProject()


### Example

```typescript
import {
    ProjectsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new ProjectsApi(configuration);

let projectId: string; // (default to undefined)

const { status, data } = await apiInstance.getProject(
    projectId
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **projectId** | [**string**] |  | defaults to undefined|


### Return type

**ProjectDTO**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |
|**404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **getProjects**
> Array<ProjectDTO> getProjects()


### Example

```typescript
import {
    ProjectsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new ProjectsApi(configuration);

const { status, data } = await apiInstance.getProjects();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**Array<ProjectDTO>**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |
|**404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

