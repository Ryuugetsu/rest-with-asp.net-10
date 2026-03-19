import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Login from "./pages/Login";
import Books from "./pages/Books";
import NewBook from "./pages/NewBook";

export default function MyRoutes() {
  return (
    <Router>
      <Routes>
        <Route path="/" Component={Login} />
        <Route path="/books" Component={Books} />
        <Route path="/books/new/:bookId" Component={NewBook} />
      </Routes>
    </Router>
  );
}