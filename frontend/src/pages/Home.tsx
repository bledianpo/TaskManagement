import { Box, Button, Text } from "@chakra-ui/react";
import { Link } from "react-router-dom";
import { GRADIENT_BG } from "../constants";

const Home = () => {
  return (
    <Box
      as="main"
      minH="100vh"
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
      px={4}
      py={12}
      bg={GRADIENT_BG}
    >
      <Text fontSize={{ base: "2xl", md: "3xl" }} fontWeight="800" color="white" mb={4} textAlign="center">
        Task Management
      </Text>
      <Text color="white" opacity={0.9} mb={8} textAlign="center" px={2}>
        Log in or create an account to manage your tasks.
      </Text>
      <Box display="flex" flexWrap="wrap" gap={4} justifyContent="center">
        <Link to="/login">
          <Button colorScheme="whiteAlpha" bg="white" color="#4f46e5" size="lg">
            Log in
          </Button>
        </Link>
        <Link to="/register">
          <Button variant="outline" borderColor="white" color="white" size="lg">
            Register
          </Button>
        </Link>
      </Box>
    </Box>
  );
};

export default Home;
