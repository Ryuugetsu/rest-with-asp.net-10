import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../services/Api";

import './styles.css';

import logoImage from '../../assets/logo.svg';
import padlock from '../../assets/padlock.png';

export default function Login() {
  const navigate = useNavigate();
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');

  async function handleLogin(e) {
    e.preventDefault();

    alert("Login clicked!"); // 👈 debug

    const data = {
      userName,
      password
    };

    try {
      const response = await api.post('/api/Home/Login', data);

      localStorage.setItem('user', data.userName);
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('tokenExpiration', response.data.tokenExpiration);

      navigate('/books');
    } catch (error) {
      console.error('Login error:', error);
    }
  }


  return (
    <div className="login-container">
      <section className="form">
        <img src={logoImage} alt="Logo" />

        <form onSubmit={handleLogin}>
          <h1>Access Your Account</h1>

          <input type="text" placeholder="Username" value={userName} onChange={(e) => setUserName(e.target.value)} />
          <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />

          <button className="button" type="submit">Login</button>
        </form>

      </section>
      <img src={padlock} alt="Padlock" />
    </div>
  );
}