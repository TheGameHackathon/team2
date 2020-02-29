import React from 'react';
import styles from './styles.css';
import Field from '../Field';

const TABLE_WIDTH = 600;
const cellsInRowAmount = 8
const cellSize = TABLE_WIDTH / cellsInRowAmount;

export default class App extends React.Component {
    constructor () {
        super();
        this.state = {
            score: 50,
        };
    }

    render () {
        return (
            <div className={ styles.root }>
                <div className={ styles.score }>
                    Ваш счет: { this.state.score }
                </div>
                <Field cellSize={cellSize}/>
            </div>
        );
    }
}

window.addEventListener("keydown", e => {
    const player = document.getElementById('player');
    if (e.keyCode >= 37 && e.keyCode <= 40) {
        let oldx = parseInt(player.style.left.slice(0, -2));
        let oldy = parseInt(player.style.top.slice(0, -2));
    
        if (e.keyCode === 37) {
            player.style.left = (oldx - cellSize) + 'px';
        }
        if (e.keyCode === 39) {
            player.style.left = (oldx + cellSize) + 'px';
        }
        if (e.keyCode === 38) {
            player.style.top = (oldy - cellSize) + 'px';
        }
        if (e.keyCode === 40) {
            player.style.top = (oldy + cellSize) + 'px';
        }
    }
});