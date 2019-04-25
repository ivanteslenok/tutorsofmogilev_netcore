import React from 'react';
import { NavLink } from 'react-router-dom';
import ListItem from '@material-ui/core/ListItem';
import { withStyles } from '@material-ui/core/styles';

import './index.css';

const styles = theme => ({
  padding: {
    padding: 0
  }
});

const MenuItem = ({ classes, to, text }) => (
  <ListItem button className={classes.padding}>
    <NavLink
      className="menu-item__nav-link"
      activeClassName="menu-item__nav-link_active"
      to={to}
    >
      {text}
    </NavLink>
  </ListItem>
);

export default withStyles(styles)(MenuItem);
