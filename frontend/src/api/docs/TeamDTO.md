# TeamDTO


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **string** |  | [default to undefined]
**mentors** | **Array&lt;string&gt;** |  | [optional] [default to undefined]
**students** | **Array&lt;string&gt;** |  | [optional] [default to undefined]
**weeklyMeetingDay** | [**DayOfWeek**](DayOfWeek.md) |  | [optional] [default to undefined]
**weeklyMeetingTime** | **string** |  | [optional] [default to undefined]
**attendance** | **Array&lt;Array&lt;string&gt;&gt;** |  | [optional] [default to undefined]

## Example

```typescript
import { TeamDTO } from './api';

const instance: TeamDTO = {
    id,
    mentors,
    students,
    weeklyMeetingDay,
    weeklyMeetingTime,
    attendance,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
