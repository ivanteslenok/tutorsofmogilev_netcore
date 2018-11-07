import React from 'react';
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import { withStyles } from '@material-ui/core/styles';

const styles = theme => ({
  focus: {
    '&:focus': {
      backgroundColor: 'inherit'
    }
  }
});

function IconBtn({ classes, title, placement, icon, handleClick }) {
  return (
    <Tooltip title={title} placement={placement}>
      <IconButton
        onClick={handleClick}
        className={classes.focus}
      >
        <i className="material-icons">{icon}</i>
      </IconButton>
    </Tooltip>
  );
}

export default withStyles(styles)(IconBtn);
