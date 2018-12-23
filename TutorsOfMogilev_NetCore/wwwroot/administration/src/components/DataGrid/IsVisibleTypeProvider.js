import React from 'react';
import { DataTypeProvider } from '@devexpress/dx-react-grid';
import Tooltip from '@material-ui/core/Tooltip';
import VisibleIcon from '@material-ui/icons/VisibilityTwoTone';
import VisibleOffIcon from '@material-ui/icons/VisibilityOffTwoTone';
import Switch from '@material-ui/core/Switch';

const IsVisibleEditor = ({ value, onValueChange }) => (
  <Switch
    checked={value}
    onChange={event => onValueChange(event.target.checked)}
    value="isVisible"
    color="default"
  />
);

const IsVisibleFromatter = ({ value }) =>
  value ? (
    <Tooltip title="Видимый">
      <VisibleIcon />
    </Tooltip>
  ) : (
    <Tooltip title="Скрыт">
      <VisibleOffIcon />
    </Tooltip>
  );

export default props => (
  <DataTypeProvider
    editorComponent={IsVisibleEditor}
    formatterComponent={IsVisibleFromatter}
    {...props}
  />
);
