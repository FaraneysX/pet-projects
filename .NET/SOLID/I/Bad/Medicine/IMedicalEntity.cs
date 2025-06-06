namespace Bad.Medicine;

/// <summary>
/// Интерфейс для медицинских сущностей, объединяющий различные аспекты медицинской деятельности.
/// </summary>
internal interface IMedicalEntity
{
    /// <summary>
    /// Проводится диагностика.
    /// </summary>
    void Diagnose();
    
    /// <summary>
    /// Проводит хирургическую операцию.
    /// </summary>
    void Operate();
    
    /// <summary>
    /// Калибрует медицинское оборудование или прибор.
    /// </summary>
    void Calibrate();
    
    /// <summary>
    /// Назначает лекарственное средство пациенту.
    /// </summary>
    void PrescribeDrug();
}