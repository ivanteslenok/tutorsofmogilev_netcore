import React from 'react';
import IconButton from '@material-ui/core/IconButton';
import SaveIcon from '@material-ui/icons/Save';
import Tooltip from '@material-ui/core/Tooltip';

const SaveBtn = ({ onExecute }) => (
  <Tooltip title="Сохранить" placement="left">
    <IconButton onClick={onExecute}>
      <SaveIcon />
    </IconButton>
  </Tooltip>
);

export default SaveBtn;
