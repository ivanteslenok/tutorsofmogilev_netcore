import React from 'react';
import Header from './Header';

import './index.css';

export default function Container({ headerText, children }) {
  return (
    <div className="app-container">
      <Header text={headerText} />
      <main>{children}</main>
    </div>
  );
}
