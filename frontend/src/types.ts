export interface User {
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
