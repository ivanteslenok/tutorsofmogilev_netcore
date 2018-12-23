import React from 'react';
import MenuItem from './MenuItem';
import List from '@material-ui/core/List';

import './index.css';

export default function Menu(params) {
  return (
    <nav>
      <h1 className="menu-head">
        <i className="material-icons left">build</i>
        Administration
      </h1>
      <List>
        <MenuItem to="/tutors" text="Репетиторы" />
        <MenuItem to="/subjects" text="Предметы" />
        <MenuItem to="/contact-types" text="Типы контактов" />
        <MenuItem to="/specializations" text="Специализации" />
        <MenuItem to="/districts" text="Районы" />
      </List>
    </nav>
  );
}
