﻿namespace BehavioralPatternApp.Commands;

public interface ICommand
{
    void Execute();
    void Undo();
}