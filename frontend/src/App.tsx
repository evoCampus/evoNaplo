import { Routes, Route } from "react-router";
import "./App.css";
import AppLayout from "./components/layouts/AppLayout";
import MentorLayout from "./components/layouts/MentorLayout";
import AdminLayout from "./components/layouts/AdminLayout";
import UpcomingMeetingsPage from "./pages/mentor/UpcomingMeetingsPage";
import ProjectDetailsPage from "./pages/mentor/ProjectDetailsPage";
import SemestersPage from "./pages/SemestersPage";
import SettingsPage from "./pages/SettingsPage";
import StudentsPage from "./pages/admin/StudentsPage";
import MentorsPage from "./pages/admin/MentorsPage";
import ProjectsPage from "./pages/admin/ProjectsPage";
import QuickActionsPage from "./pages/admin/QuickActionsPage";
import NotFoundPage from "./pages/NotFoundPage";
import { Navigate } from "react-router";

function App() {
  return (
    <Routes>
      <Route element={<AppLayout />}>
        <Route path="mentor" element={<MentorLayout />}>
          <Route index element={<Navigate to="meetings" />} />
          <Route path="meetings" element={<UpcomingMeetingsPage />} />
          <Route path="projects/:id" element={<ProjectDetailsPage />} />
          <Route path="semesters" element={<SemestersPage />} />
          <Route path="settings" element={<SettingsPage />} />
        </Route>
        <Route path="admin" element={<AdminLayout />}>
          <Route index path="" element={<QuickActionsPage />} />
          <Route path="students" element={<StudentsPage />} />
          <Route path="mentors" element={<MentorsPage />} />
          <Route path="projects" element={<ProjectsPage />} />
          <Route path="semesters" element={<SemestersPage />} />
          <Route path="settings" element={<SettingsPage />} />
        </Route>
        <Route path="*" element={<NotFoundPage />} />
      </Route>
    </Routes>
  );
}

export default App;
