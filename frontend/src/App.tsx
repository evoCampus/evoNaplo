import { Routes, Route } from "react-router";
import type { User } from "./types";

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


function App() {
  const MOCK_USER: User = {
    name: "Teszt Admin",
    role: "mentor",
    email: "admin@test.com"
  };

  return (
    <Routes>
      <Route element={<AppLayout />}>
        <Route index element={<AuthPage />} />
        <Route element={<DashboardLayout user={MOCK_USER} />}>
            <Route element={<ProtectedRoute user={MOCK_USER} allowedRoles={['mentor']} />}>
                <Route path="mentor" >
                    <Route index element={<MentorHomePage />} />
                    <Route path="meetings" element={<UpcomingMeetingsPage />} />
                    <Route path="projects/:id" element={<ProjectDetailsPage />} />
                    <Route path="semesters" element={<SemestersPage />} />
                    <Route path="settings" element={<SettingsPage />} />
                </Route>
            </Route>
            <Route element={<ProtectedRoute user={MOCK_USER} allowedRoles={['admin']} />}>
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
  );
}

export default App;
