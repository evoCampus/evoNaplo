import { DayOfWeek } from "../api";

/**
 * Standard day names array aligned with .NET DayOfWeek enum (0 = Sunday, 1 = Monday, ...)
 */
export const DAYS_OF_WEEK = [
  "Sunday",
  "Monday",
  "Tuesday",
  "Wednesday",
  "Thursday",
  "Friday",
  "Saturday",
];

/**
 * Returns the name of the day based on the backend DayOfWeek enum value.
 */
export function getDayName(day?: DayOfWeek): string {
  if (day === undefined || day === null) return "TBD";
  return DAYS_OF_WEEK[day] || "TBD";
}

/**
 * Calculates the upcoming meeting date based on the target day of week.
 * Ensures the date is consistent across the application.
 */
export function getMeetingDates(dayOfWeek: DayOfWeek) {
  const targetJsDay = dayOfWeek as number; // 0 = Sunday, 1 = Monday...
  const today = new Date();
  const currentDay = today.getDay(); // 0 is Sunday

  // Calculate days until the next occurrence (including today)
  const daysUntil = (targetJsDay - currentDay + 7) % 7;

  const meetingDate = new Date(today);
  meetingDate.setDate(today.getDate() + daysUntil);

  const year = meetingDate.getFullYear();
  const month = String(meetingDate.getMonth() + 1).padStart(2, '0');
  const day = String(meetingDate.getDate()).padStart(2, '0');
  const dayName = DAYS_OF_WEEK[targetJsDay];

  return {
    formatted: `${year}. ${month}. ${day}. ${dayName}`,
    raw: `${year}-${month}-${day}`
  };
}

/**
 * Formats a TimeSpan string (HH:mm:ss) to HH:mm.
 */
export function formatTime(timeStr?: string) {
  if (!timeStr) return "TBD";
  return timeStr.substring(0, 5);
}
