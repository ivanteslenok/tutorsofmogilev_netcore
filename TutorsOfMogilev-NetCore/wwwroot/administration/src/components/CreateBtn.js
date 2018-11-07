import React from 'react';
import Button from '@material-ui/core/Button';
import classNames from 'classnames';
import { withStyles } from '@material-ui/core/styles';
import teal from '@material-ui/core/colors/teal';

const styles = theme => ({
  teal: {
    color: theme.palette.getContrastText(teal[500]),
    backgroundColor: teal[500],
    '&:hover': {
      backgroundColor: teal[700]
    }
  },
  margin: {
    margin: '10px'
  }
});

function CreateBtn({ classes, handleClick }) {
  return (
    <Button
      onClick={handleClick}
      variant="contained"
      className={classNames(classes.teal, classes.margin)}
    >
      Создать
      <i className="material-icons right">add</i>
    </Button>
  );
}

export default withStyles(styles)(CreateBtn);
