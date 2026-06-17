# DataApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**dataExportGet**](#dataexportget) | **GET** /Data/export | |
|[**dataImportPost**](#dataimportpost) | **POST** /Data/import | |

# **dataExportGet**
> dataExportGet()


### Example

```typescript
import {
    DataApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new DataApi(configuration);

let filterTimestamp: string; // (optional) (default to undefined)
let filterName: string; // (optional) (default to undefined)
let filterEmail: string; // (optional) (default to undefined)
let filterPhoneNumber: string; // (optional) (default to undefined)
let filterMajor: string; // (optional) (default to undefined)
let filterIsFirstTime: string; // (optional) (default to undefined)
let filterGoals: string; // (optional) (default to undefined)
let filterStayInTeam: string; // (optional) (default to undefined)
let filterOtherComments: string; // (optional) (default to undefined)
let rowCount: number; // (optional) (default to undefined)
let format: ExportFormat; // (optional) (default to undefined)

const { status, data } = await apiInstance.dataExportGet(
    filterTimestamp,
    filterName,
    filterEmail,
    filterPhoneNumber,
    filterMajor,
    filterIsFirstTime,
    filterGoals,
    filterStayInTeam,
    filterOtherComments,
    rowCount,
    format
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **filterTimestamp** | [**string**] |  | (optional) defaults to undefined|
| **filterName** | [**string**] |  | (optional) defaults to undefined|
| **filterEmail** | [**string**] |  | (optional) defaults to undefined|
| **filterPhoneNumber** | [**string**] |  | (optional) defaults to undefined|
| **filterMajor** | [**string**] |  | (optional) defaults to undefined|
| **filterIsFirstTime** | [**string**] |  | (optional) defaults to undefined|
| **filterGoals** | [**string**] |  | (optional) defaults to undefined|
| **filterStayInTeam** | [**string**] |  | (optional) defaults to undefined|
| **filterOtherComments** | [**string**] |  | (optional) defaults to undefined|
| **rowCount** | [**number**] |  | (optional) defaults to undefined|
| **format** | **ExportFormat** |  | (optional) defaults to undefined|


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

# **dataImportPost**
> dataImportPost()


### Example

```typescript
import {
    DataApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new DataApi(configuration);

let file: File; // (optional) (default to undefined)

const { status, data } = await apiInstance.dataImportPost(
    file
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **file** | [**File**] |  | (optional) defaults to undefined|


### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

