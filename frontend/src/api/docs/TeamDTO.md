# TeamDTO


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **string** |  | [default to undefined]
**mentorIds** | **Array&lt;string&gt;** |  | [default to undefined]
**studentIds** | **Array&lt;string&gt;** |  | [default to undefined]
**weeklyMeetingDay** | [**DayOfWeek**](DayOfWeek.md) |  | [optional] [default to undefined]
**weeklyMeetingTime** | **string** |  | [optional] [default to undefined]
**attendanceSheetIds** | **Array&lt;string&gt;** |  | [default to undefined]

## Example

```typescript
import { TeamDTO } from './api';

const instance: TeamDTO = {
    id,
    mentorIds,
    studentIds,
    weeklyMeetingDay,
    weeklyMeetingTime,
    attendanceSheetIds,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
