import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { BrowserRouter } from "react-router";
import App from "./App.tsx";
import { ApiClientProvider } from "./components/ApiClientProvider";
import { SemesterProvider } from "./components/SemesterProvider";
import { UserProvider } from "./components/UserProvider";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <UserProvider>
      <ApiClientProvider>
        <SemesterProvider>
          <BrowserRouter>
            <App />
          </BrowserRouter>
        </SemesterProvider>
      </ApiClientProvider>
    </UserProvider>
  </StrictMode>,
);
