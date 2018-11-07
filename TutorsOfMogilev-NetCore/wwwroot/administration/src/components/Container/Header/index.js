import React from 'react';
import { defaultUrl } from '../../../constants';
import Tooltip from '@material-ui/core/Tooltip';

import './index.css';

export default function Header({ text }) {
  return (
    <header>
      <h2 className="head">{text}</h2>
      <Tooltip title="Вернуться на сайт" placement="left">
        <a href={defaultUrl} className="exit">
          <i className="material-icons">exit_to_app</i>
        </a>
      </Tooltip>
    </header>
  );
}
