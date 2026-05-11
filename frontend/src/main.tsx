import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { BrowserRouter } from "react-router";
import App from "./App.tsx";
import { ApiClientProvider } from "./components/ApiClientProvider";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ApiClientProvider>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </ApiClientProvider>
  </StrictMode>,
);
