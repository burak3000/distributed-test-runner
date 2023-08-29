import logo from "./logo.svg";
import "./App.css";
import Login from "./Login";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import SignUp from "./SignUp";
import axios from "axios";

function App() {
  axios.defaults.baseURL = process.env.REACT_APP_API_URL;
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />}></Route>
      </Routes>
      <Routes>
        <Route path="/sign-up" element={<SignUp />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
