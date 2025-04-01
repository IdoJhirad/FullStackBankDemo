import React from 'react';
import './Layout.css'; 

export function Layout({ children }) {
  return (
    <>
      <header className="app-header">
        <h1>Bank</h1>
      </header>
      <main className="app-main">
        {children}
      </main>
    </>
  );
}
