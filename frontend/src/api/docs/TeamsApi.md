# TeamsApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiTeamsGet**](#apiteamsget) | **GET** /api/Teams | |
|[**apiTeamsPost**](#apiteamspost) | **POST** /api/Teams | |
|[**apiTeamsTeamIdDelete**](#apiteamsteamiddelete) | **DELETE** /api/Teams/{teamId} | |
|[**apiTeamsTeamIdGet**](#apiteamsteamidget) | **GET** /api/Teams/{teamId} | |
|[**apiTeamsTeamIdPut**](#apiteamsteamidput) | **PUT** /api/Teams/{teamId} | |

# **apiTeamsGet**
> Array<TeamDTO> apiTeamsGet()


### Example

```typescript
import {
    TeamsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new TeamsApi(configuration);

const { status, data } = await apiInstance.apiTeamsGet();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**Array<TeamDTO>**

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

# **apiTeamsPost**
> TeamDTO apiTeamsPost()


### Example

```typescript
import {
    TeamsApi,
    Configuration,
    CreateTeamDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new TeamsApi(configuration);

let createTeamDTO: CreateTeamDTO; // (optional)

const { status, data } = await apiInstance.apiTeamsPost(
    createTeamDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **createTeamDTO** | **CreateTeamDTO**|  | |


### Return type

**TeamDTO**

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

# **apiTeamsTeamIdDelete**
> apiTeamsTeamIdDelete()


### Example

```typescript
import {
    TeamsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new TeamsApi(configuration);

let teamId: string; // (default to undefined)

const { status, data } = await apiInstance.apiTeamsTeamIdDelete(
    teamId
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **teamId** | [**string**] |  | defaults to undefined|


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

# **apiTeamsTeamIdGet**
> TeamDTO apiTeamsTeamIdGet()


### Example

```typescript
import {
    TeamsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new TeamsApi(configuration);

let teamId: string; // (default to undefined)
let includeStudents: boolean; // (optional) (default to false)

const { status, data } = await apiInstance.apiTeamsTeamIdGet(
    teamId,
    includeStudents
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **teamId** | [**string**] |  | defaults to undefined|
| **includeStudents** | [**boolean**] |  | (optional) defaults to false|


### Return type

**TeamDTO**

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

# **apiTeamsTeamIdPut**
> apiTeamsTeamIdPut()


### Example

```typescript
import {
    TeamsApi,
    Configuration,
    UpdateTeamDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new TeamsApi(configuration);

let teamId: string; // (default to undefined)
let updateTeamDTO: UpdateTeamDTO; // (optional)

const { status, data } = await apiInstance.apiTeamsTeamIdPut(
    teamId,
    updateTeamDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **updateTeamDTO** | **UpdateTeamDTO**|  | |
| **teamId** | [**string**] |  | defaults to undefined|


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

