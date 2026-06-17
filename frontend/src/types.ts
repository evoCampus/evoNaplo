import { type TeamDTO, type ProjectDTO } from "./api";

export interface User {
  id: string;
  name: string;
  role: UserRole;
  email: string;
}

export type UserRole = 'admin' | 'mentor';

export interface ProtectedRouteProps {
  user: User;
  allowedRoles: UserRole[];
}

export interface SidebarProps {
  user: User;
}

export interface UIMentorProject {
  id: string;
  name: string;
  subItems: { title: string; url: string }[];
}

export interface TeamWithMembers extends TeamDTO {
  memberNames: string[];
}

export interface ProjectDetailedData {
  project: ProjectDTO;
  teams: TeamWithMembers[];
}

export interface UIStudent {
  id: string;
  name: string;
  isPresent?: boolean;
}

export interface UIMeeting {
  id: string;
  projectId: string;
  projectName: string;
  teamName: string;
  location: string;
  date: string;
  rawDate: string;
  time: string;
  students: UIStudent[];
}

export interface UITeam {
  id: string;
  name: string;
  projectId: string;
}

export interface MentorHomeData {
  mentorName: string;
  teams: UITeam[];
}

export interface UpcomingMeetingsData {
  mentorName: string;
  meetings: UIMeeting[];
}
