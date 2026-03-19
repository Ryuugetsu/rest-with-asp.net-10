import React, { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
// import { useNavigate } from "react-router-dom";

import api from "../../services/Api";

import './styles.css';

import { FiArrowLeft } from "react-icons/fi";
import logoImage from '../../assets/logo.svg';

export default function NewBook() {
  const [title, setTitle] = useState('');
  const [author, setAuthor] = useState('');
  const [price, setPrice] = useState('');
  const [launchDate, setLaunchDate] = useState('');
  const [id, setId] = useState(0);

  const navigate = useNavigate();
  const { bookId } = useParams();

  const acessToken = localStorage.getItem('accessToken');
  const authorization = {
    headers: {
      Authorization: `Bearer ${acessToken}`,
    }
  }

  useEffect(() => {
    if (bookId === '0') return;
    debugger;
    loadBook();
  }, bookId);

  async function loadBook() {
    try {
      const response = await api.get(`/api/book/${bookId}`, authorization);
      const book = response.data;

      let adjustedLaunchDate = book.launchDate.split('T')[0];

      setId(book.id);
      setTitle(book.title);
      setAuthor(book.author);
      setPrice(book.price);
      setLaunchDate(adjustedLaunchDate);
    } catch (err) {
      alert('Error loading book, please try again.');
    }
  }

  async function saveOrUpdate(e) {
    e.preventDefault();

    const data = {
      title,
      author,
      price,
      launchDate,
    };

    try {
      //- Da para fazer sem o if pois para o backend não tem diferença entre criar ou atualizar, basta enviar o id ou não. 
      //- Mas para deixar mais claro, deixei o if.
      if (bookId !== '0') {
        data.id = id;
        await api.put('/api/book', data, authorization);
      } else {
        await api.post('/api/book', data, authorization);
      }
      navigate('/books');
    } catch (err) {
      alert('Error registering book, please try again.');
    }
  }

  return (
    <div className="new-book-container">
      <div className="content">
        <section className="form">
          <img src={logoImage} alt="Logo" />
          <h1>{bookId !== '0' ? 'Update Book' : 'Register New Book'}</h1>
          <p>Fill in the details of the book you want to {bookId !== '0' ? 'update' : 'register'}.</p>

          <Link className="back-link" to="/books">
            <FiArrowLeft size={16} color="#251fc5" />
            Back
          </Link>
        </section>

        <form onSubmit={saveOrUpdate}>
          <input placeholder="Title" value={title} onChange={e => setTitle(e.target.value)} />
          <input placeholder="Author" value={author} onChange={e => setAuthor(e.target.value)} />
          <input placeholder="Price" value={price} onChange={e => setPrice(e.target.value)} />
          <input type="date" value={launchDate} onChange={e => setLaunchDate(e.target.value)} />

          <button className="button" type="submit">{bookId !== '0' ? 'Update' : 'Register'}</button>
        </form>

      </div>
    </div>
  );
}