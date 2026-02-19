import { Routes, Route } from "react-router-dom";
import { Box } from "@chakra-ui/react";
import Login from "./pages/Login";
import Home from "./pages/Home";
import Navbar from "./components/Navbar";
import Register from "./pages/Register";
import Tasks from "./pages/Tasks";

function App() {
  return (
    <Box>
      <Navbar />

      <Routes>
        <Route path="/" element={<Home />}></Route>
        <Route path="/login" element={<Login />}></Route>
        <Route path="/register" element={<Register />}></Route>
        <Route path="/tasks" element={<Tasks />}></Route>
      </Routes>
    </Box>
  );
}

export default App;
