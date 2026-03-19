import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { FiPower, FiEdit, FiTrash2 } from "react-icons/fi";
import api from "../../services/Api";

import './styles.css';

import logoImage from '../../assets/logo.svg';

export default function Books() {

  const navigate = useNavigate();

  const userName = localStorage.getItem('userName');
  const accessToken = localStorage.getItem('accessToken');
  const authorization = {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    }
  };

  const [books, setBooks] = useState([]);
  const [page, setPage] = useState(1);

  useEffect(() => {
    fetchMoreBooks();
  }, [accessToken]);

  async function fetchMoreBooks() {
    // Fetch books from API
    try {
      // debugger;
      const response = await api.get('/api/book/Paged', {
        headers: authorization.headers,
        params: {
          page: page,
          perPage: 4,
        },
      });
      
      setBooks([...books, ...response.data]);
      setPage(page + 1);
    }
    catch (err) {
      alert('Error fetching books, please try again.' + err);
    };
  }


  async function editBook(id) {
    try {
      navigate(`/books/new/${id}`);
    } catch (err) {
      alert('Error navigating to edit book, please try again.' + err);
    }
  }


  async function deleteBook(id) {
    try {
      await api.delete(`/api/book/${id}`, authorization);
      // Remove the deleted book from the list
      setBooks(books.filter(book => book.id !== id));
    } catch (err) {
      alert('Error deleting book, please try again.' + err);
    }
  }

  async function logout() {
    try {
      // await api.delete(`/api/book/logout`, authorization);

      localStorage.clear();
      navigate('/login');
    } catch (err) {
      alert('Error logging out, please try again.' + err);
    }
  }

  return (
    <div className="book-container">
      <header>
        <img src={logoImage} alt="Logo" />
        <span>Welcome, <strong>{userName?.toUpperCase()}</strong></span>
        <Link className="button" to="/books/new/0">Add New Book</Link>
        <button onClick={logout} type="button">
          <FiPower size={18} color="#251fc5" />
        </button>
      </header>

      <h1>Book List</h1>

      <ul>
        {books.map(book => (
          <li key={book.id}>
            <strong>Title:</strong>
            <p>{book.title}</p>
            <strong>Author:</strong>
            <p>{book.author}</p>
            <strong>Price:</strong>
            <p>{Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(book.price)}</p>
            <strong>Release Date:</strong>
            <p>{Intl.DateTimeFormat('pt-BR').format(new Date(book.launchDate))}</p>

            <button onClick={() => editBook(book.id)} type="button">
              <FiEdit size={20} color="#251fc5" />
            </button>
            <button onClick={() => deleteBook(book.id)} type="button">
              <FiTrash2 size={20} color="#251fc5" />
            </button>
          </li>
        ))}
      </ul>

      <button className="button" type="button" onClick={fetchMoreBooks}>Load More</button>
    </div>
  );
}