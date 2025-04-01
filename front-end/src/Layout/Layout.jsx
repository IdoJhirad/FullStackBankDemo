import React from 'react';
import './Layout.css'; 

export function Layout({ children }) {
  return (
    <>
      <header className="app-header">
        <h1>Bank Project</h1>
      </header>
      <main className="app-main">
        {children}
      </main>
    </>
  );
}
