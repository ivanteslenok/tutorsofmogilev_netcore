import React from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';
import { withStyles } from '@material-ui/core/styles';
import teal from '@material-ui/core/colors/teal';

const styles = theme => ({
  teal: {
    color: teal[500]
  },
  outer: {
    position: 'absolute',
    width: '100%',
    height: '100%',
    top: 0,
    background: 'rgba(255, 255, 255, .5)'
  },
  centrilize: {
    position: 'absolute',
    margin: 'auto',
    top: '0',
    right: '0',
    bottom: '0',
    left: '0',
    width: '50px',
    height: '50px'
  }
});

function Loading({ classes }) {
  return (
    <div className={classes.outer}>
      <div className={classes.centrilize}>
        <CircularProgress className={classes.teal} size={50} />
      </div>
    </div>
  );
}

export default withStyles(styles)(Loading);
