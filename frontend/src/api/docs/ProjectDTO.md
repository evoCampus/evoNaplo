# ProjectDTO


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **string** |  | [default to undefined]
**name** | **string** |  | [optional] [default to undefined]
**description** | **string** |  | [optional] [default to undefined]
**projectLinks** | **{ [key: string]: string; }** |  | [optional] [default to undefined]
**teams** | **Array&lt;string&gt;** |  | [optional] [default to undefined]

## Example

```typescript
import { ProjectDTO } from './api';

const instance: ProjectDTO = {
    id,
    name,
    description,
    projectLinks,
    teams,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
