// gorapulv-frontend/src/App.jsx
import React, { useEffect, useState } from 'react';
import Article from './components/Article';
function App() {
    const [articles, setArticles] = useState([]);
    useEffect(() => {
            fetch("/api/Articles")
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Erreur API: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                setArticles(data);
            })
            .catch(error => {
                console.error("Erreur lors de la récupération des articles :",
                    error);
            });
        },
        []);
    return (
        <div className="App">
            <h1>Liste des articlesssss</h1>
            {articles.length === 0 ? (
                <p>Aucun article n'est disponible.</p>
            ) : (
                articles.map(article => (
                    <Article
                        key={article.id}
                        title={article.title}
                        content={article.content}
                        author={article.author}
                        createdAt={article.createdAt}
                    />
                ))
            )}
        </div>
    );
}
export default App;