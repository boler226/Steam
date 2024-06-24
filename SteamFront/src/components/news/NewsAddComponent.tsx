import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './style/NewsAddComponent.css';

const NewsAddComponent = () => {
  const [title, setTitle] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [dateOfRelease, setDateOfRelease] = useState<string>(new Date().toISOString().split('T')[0]);
  const [image, setImage] = useState<File | null>(null);
  const [video, setVideo] = useState<File | null>(null);;
  const [gameId, setGameId] = useState<string>('');
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      const formData = new FormData();
      formData.append('title', title);
      formData.append('description', description);
      formData.append('dateOfRelease', dateOfRelease);
      if (image) formData.append('image', image);
      if (video) formData.append('video', video);
      formData.append('gameId', gameId);

      await axios.post('http://localhost:5002/api/News', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });
      navigate('/');
      console.log('Новина додана успішно!');
    } catch (error) {
      console.error('Помилка при створені новини!', error);
    }
  };

  return (
    <div className="news-add-container">
    <form className="news-add-form" onSubmit={handleSubmit}>
      <div className="form-group">
        <label>Заголовок:</label>
        <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} required />
      </div>
      <div className="form-group">
        <label>Опис:</label>
        <textarea value={description} onChange={(e) => setDescription(e.target.value)} required />
      </div>
      <div className="form-group">
          <label>Фото:</label>
          <input type="file" onChange={(e) => setImage(e.target.files ? e.target.files[0] : null)} required />
        </div>
        <div className="form-group">
          <label>Відео:</label>
          <input type="file" onChange={(e) => setVideo(e.target.files ? e.target.files[0] : null)} accept="video/*" required />
        </div>
      <div className="form-group">
        <label>ID гри:</label>
        <input type="text" value={gameId} onChange={(e) => setGameId(e.target.value)} required />
      </div>
      <button type="submit" className="submit-button">Додати новину</button>
    </form>
  </div>
  );
};

export default NewsAddComponent;