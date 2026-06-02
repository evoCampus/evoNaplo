# MentorsApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**apiMentorsGet**](#apimentorsget) | **GET** /api/Mentors | |
|[**apiMentorsMentorIdDelete**](#apimentorsmentoriddelete) | **DELETE** /api/Mentors/{mentorId} | |
|[**apiMentorsMentorIdGet**](#apimentorsmentoridget) | **GET** /api/Mentors/{mentorId} | |
|[**apiMentorsMentorIdPut**](#apimentorsmentoridput) | **PUT** /api/Mentors/{mentorId} | |
|[**apiMentorsPost**](#apimentorspost) | **POST** /api/Mentors | |

# **apiMentorsGet**
> Array<MentorDTO> apiMentorsGet()


### Example

```typescript
import {
    MentorsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new MentorsApi(configuration);

const { status, data } = await apiInstance.apiMentorsGet();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**Array<MentorDTO>**

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

# **apiMentorsMentorIdDelete**
> apiMentorsMentorIdDelete()


### Example

```typescript
import {
    MentorsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new MentorsApi(configuration);

let mentorId: string; // (default to undefined)

const { status, data } = await apiInstance.apiMentorsMentorIdDelete(
    mentorId
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **mentorId** | [**string**] |  | defaults to undefined|


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

# **apiMentorsMentorIdGet**
> MentorDTO apiMentorsMentorIdGet()


### Example

```typescript
import {
    MentorsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new MentorsApi(configuration);

let mentorId: string; // (default to undefined)

const { status, data } = await apiInstance.apiMentorsMentorIdGet(
    mentorId
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **mentorId** | [**string**] |  | defaults to undefined|


### Return type

**MentorDTO**

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

# **apiMentorsMentorIdPut**
> apiMentorsMentorIdPut()


### Example

```typescript
import {
    MentorsApi,
    Configuration,
    MentorDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new MentorsApi(configuration);

let mentorId: string; // (default to undefined)
let mentorDTO: MentorDTO; // (optional)

const { status, data } = await apiInstance.apiMentorsMentorIdPut(
    mentorId,
    mentorDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **mentorDTO** | **MentorDTO**|  | |
| **mentorId** | [**string**] |  | defaults to undefined|


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

# **apiMentorsPost**
> MentorDTO apiMentorsPost()


### Example

```typescript
import {
    MentorsApi,
    Configuration,
    MentorDTO
} from './api';

const configuration = new Configuration();
const apiInstance = new MentorsApi(configuration);

let mentorDTO: MentorDTO; // (optional)

const { status, data } = await apiInstance.apiMentorsPost(
    mentorDTO
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **mentorDTO** | **MentorDTO**|  | |


### Return type

**MentorDTO**

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

