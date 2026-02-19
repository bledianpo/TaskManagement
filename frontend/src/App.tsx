import { Routes, Route } from "react-router-dom";
import { Box, Heading} from "@chakra-ui/react";
import Login from "./pages/Login";
import Register from "./pages/Regsiter";
import Home from "./pages/Home";

function App() {
  return (
    <Box>
      <Heading color="red.500">Task Management</Heading>

      <Routes>
        <Route path="/" element={<Home/>}></Route>
        <Route path="/login" element={<Login/>}></Route>
        <Route path="/register" element={<Register />}></Route>
      </Routes>
    </Box>
  );
}

export default App;
