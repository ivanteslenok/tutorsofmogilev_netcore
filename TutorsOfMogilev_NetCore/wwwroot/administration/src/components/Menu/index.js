import React from 'react';
import { defaultUrl } from '../../constants';
import MenuItem from './MenuItem';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';

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
        <MenuItem to="/cities" text="Города" />
        <ListItem
          button
          style={{ padding: 0, borderTop: '1px solid #ddd', marginTop: '20px' }}
        >
          <a href={defaultUrl} className="menu-item__nav-link">Вернуться на сайт</a>
        </ListItem>
      </List>
    </nav>
  );
}
