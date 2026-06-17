# MentorsApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**createMentor**](#creatementor) | **POST** /api/Mentors | |
|[**deleteMentor**](#deletementor) | **DELETE** /api/Mentors/{mentorId} | |
|[**getMentor**](#getmentor) | **GET** /api/Mentors/{mentorId} | |
|[**getMentors**](#getmentors) | **GET** /api/Mentors | |
|[**updateMentor**](#updatementor) | **PUT** /api/Mentors/{mentorId} | |

# **createMentor**
> MentorDTO createMentor()


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

const { status, data } = await apiInstance.createMentor(
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
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |
|**409** | Conflict |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **deleteMentor**
> deleteMentor()


### Example

```typescript
import {
    MentorsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new MentorsApi(configuration);

let mentorId: string; // (default to undefined)

const { status, data } = await apiInstance.deleteMentor(
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
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |
|**404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **getMentor**
> MentorDTO getMentor()


### Example

```typescript
import {
    MentorsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new MentorsApi(configuration);

let mentorId: string; // (default to undefined)

const { status, data } = await apiInstance.getMentor(
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
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |
|**404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **getMentors**
> Array<MentorDTO> getMentors()


### Example

```typescript
import {
    MentorsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new MentorsApi(configuration);

const { status, data } = await apiInstance.getMentors();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**Array<MentorDTO>**

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

# **updateMentor**
> updateMentor()


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

const { status, data } = await apiInstance.updateMentor(
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
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |
|**404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

