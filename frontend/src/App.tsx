import { Routes, Route } from "react-router";
import { Loader2 } from "lucide-react";

import ProtectedRoute from "./components/ProtectedRoute";
import AuthPage from "./pages/AuthPage";

import AppLayout from "./components/layouts/AppLayout";
import DashboardLayout from "./components/layouts/DashboardLayout";

import MentorHomePage from "./pages/mentor/MentorHomePage";
import UpcomingMeetingsPage from "./pages/mentor/UpcomingMeetingsPage";
import ProjectDetailsPage from "./pages/mentor/ProjectDetailsPage";

import StudentsPage from "./pages/admin/StudentsPage";
import MentorsPage from "./pages/admin/MentorsPage";
import ProjectsPage from "./pages/admin/ProjectsPage";
import AdminHomePage from "./pages/admin/AdminHomePage";
import TeamsPage from "./pages/admin/TeamsPage";

import SemestersPage from "./pages/SemestersPage";
import SettingsPage from "./pages/SettingsPage";
import NotFoundPage from "./pages/NotFoundPage";
import ErrorBoundary from "./components/ErrorBoundary";


import { useUser } from "./hooks/use-user";


function App() {
  const { user, isLoading } = useUser();

  if (isLoading || !user) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <Loader2 className="w-10 h-10 animate-spin text-primary" />
      </div>
    );
  }

  return (
    <ErrorBoundary>
      <Routes>
        <Route element={<AppLayout />}>
          <Route index element={<AuthPage />} />
          <Route element={<DashboardLayout />}>
              <Route element={<ProtectedRoute user={user} allowedRoles={['mentor']} />}>
                  <Route path="mentor" >
                      <Route index element={<MentorHomePage />} />
                      <Route path="meetings" element={<UpcomingMeetingsPage />} />
                      <Route path="projects/:id" element={<ProjectDetailsPage />} />
                      <Route path="semesters" element={<SemestersPage />} />
                      <Route path="settings" element={<SettingsPage />} />
                  </Route>
              </Route>
              <Route element={<ProtectedRoute user={user} allowedRoles={['admin', 'mentor']} />}>
                  <Route path="admin">
                      <Route index element={<AdminHomePage />} />
                      <Route path="students" element={<StudentsPage />} />
                      <Route path="mentors" element={<MentorsPage />} />
                      <Route path="teams" element={<TeamsPage />} />
                      <Route path="projects" element={<ProjectsPage />} />
                      <Route path="semesters" element={<SemestersPage />} />
                      <Route path="settings" element={<SettingsPage />} />
                  </Route>
              </Route>
          </Route>
          <Route path="*" element={<NotFoundPage />} />
        </Route>
      </Routes>
    </ErrorBoundary>
  );
}

export default App;
