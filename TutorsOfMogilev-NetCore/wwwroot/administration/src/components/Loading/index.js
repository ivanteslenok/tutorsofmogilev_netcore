import React from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';
import { withStyles } from '@material-ui/core/styles';
import teal from '@material-ui/core/colors/teal';

const styles = theme => ({
  teal: {
    color: teal[500]
  },
  centrilize: {
    margin: '0 auto',
    width: '50px',
    padding: '20px'
  }
});

function Loading({ classes }) {
  return (
    <div className={classes.centrilize}>
      <CircularProgress className={classes.teal} size={50} />
    </div>
  );
}

export default withStyles(styles)(Loading);
