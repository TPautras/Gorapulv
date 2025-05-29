// gorapulv-frontend/src/components/Article.jsx
import React from 'react';
const Article = ({ title, content, author, createdAt }) => {
    return (
        <article style={{ border: '1px solid #ccc', padding: '1em', margin: '1em 0' }}>
                <h2>{title}</h2>
            <p>{content}</p>
    <p><em>Auteur : {author}</em></p>
    {createdAt && <p><small>Publié le {new
    Date(createdAt).toLocaleString()}</small></p>}
</article>
);
};
    export default Article;
