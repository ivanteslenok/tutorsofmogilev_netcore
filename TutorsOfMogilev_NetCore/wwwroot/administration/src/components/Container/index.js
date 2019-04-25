import React from 'react';
import Header from './Header';

import './index.css';

const Container = ({ headerText, children }) => (
  <div className="app-container">
    <Header text={headerText} />
    <main>{children}</main>
  </div>
);

export default Container;
