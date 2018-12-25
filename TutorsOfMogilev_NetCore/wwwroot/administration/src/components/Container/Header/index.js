import React from 'react';
import { defaultUrl, logoutUrl } from '../../../constants';
import Tooltip from '@material-ui/core/Tooltip';

import './index.css';

export default function Header({ text }) {
  return (
    <header>
      <h2 className="head">{text}</h2>
      <Tooltip title="Выход" placement="left">
        <a href={`${defaultUrl}${logoutUrl}`} className="exit">
          <i className="material-icons">exit_to_app</i>
        </a>
      </Tooltip>
    </header>
  );
}
