# TeamsApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiTeamsGet**](#apiteamsget) | **GET** /api/Teams | |
|[**apiTeamsIdDelete**](#apiteamsiddelete) | **DELETE** /api/Teams/{id} | |
|[**apiTeamsIdGet**](#apiteamsidget) | **GET** /api/Teams/{id} | |
|[**apiTeamsIdPut**](#apiteamsidput) | **PUT** /api/Teams/{id} | |
|[**apiTeamsPost**](#apiteamspost) | **POST** /api/Teams | |

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

# **apiTeamsIdDelete**
> apiTeamsIdDelete()


### Example

```typescript
import {
    TeamsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new TeamsApi(configuration);

let id: string; // (default to undefined)

const { status, data } = await apiInstance.apiTeamsIdDelete(
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

# **apiTeamsIdGet**
> TeamDTO apiTeamsIdGet()


### Example

```typescript
import {
    TeamsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new TeamsApi(configuration);

let id: string; // (default to undefined)

const { status, data } = await apiInstance.apiTeamsIdGet(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**string**] |  | defaults to undefined|


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

# **apiTeamsIdPut**
> apiTeamsIdPut()


### Example

```typescript
import {
    TeamsApi,
    Configuration,
    TeamDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new TeamsApi(configuration);

let id: string; // (default to undefined)
let teamDTO: TeamDTO; // (optional)

const { status, data } = await apiInstance.apiTeamsIdPut(
    id,
    teamDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **teamDTO** | **TeamDTO**|  | |
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

# **apiTeamsPost**
> TeamDTO apiTeamsPost()


### Example

```typescript
import {
    TeamsApi,
    Configuration,
    TeamDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new TeamsApi(configuration);

let teamDTO: TeamDTO; // (optional)

const { status, data } = await apiInstance.apiTeamsPost(
    teamDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **teamDTO** | **TeamDTO**|  | |


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

