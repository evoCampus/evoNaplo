import { Navigate, Outlet } from 'react-router';
import type { ProtectedRouteProps } from '../types';

export default function ProtectedRoute({ user, allowedRoles }: ProtectedRouteProps) {
  if (!allowedRoles.includes(user.role)) {
    return <Navigate to={user.role === 'admin' ? '/admin' : '/mentor'} replace />;
  }
  return <Outlet />;
};
