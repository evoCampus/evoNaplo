import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";
import path from "path";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
  resolve: {
    alias: {
      "react": path.resolve(__dirname, "node_modules/react"),
      "react-dom": path.resolve(__dirname, "node_modules/react-dom"),
      "react/jsx-runtime": path.resolve(__dirname, "node_modules/react/jsx-runtime"),

      "src": path.resolve(__dirname, "./src"),
      "@/lib": path.resolve(__dirname, "../ui-library/lib"),
      "@/hooks": path.resolve(__dirname, "../ui-library/hooks"),
      "@/components": path.resolve(__dirname, "../ui-library/components"),
      "@evonaplo/ui": path.resolve(__dirname, "../ui-library"),
    },
  },
  server: { 
    fs: {
      allow: [".."],
    },
  }
});
