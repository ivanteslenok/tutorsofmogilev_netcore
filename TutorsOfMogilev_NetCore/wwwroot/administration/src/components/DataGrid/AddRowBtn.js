import React from 'react';
import Button from '@material-ui/core/Button';
import Tooltip from '@material-ui/core/Tooltip';

const AddRowBtn = ({ onExecute }) => (
  <div style={{ textAlign: 'center' }}>
    <Tooltip title="Добавить репетитора" placement="top">
      <Button style={{ color: '#03A9F4' }} onClick={onExecute}>
        Добавить
      </Button>
    </Tooltip>
  </div>
);

export default AddRowBtn;
