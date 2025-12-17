namespace SchoolJournal
{
    public enum UserRole
    {
        // 0 залишили як "Director" (раніше було Admin), щоб не ламати стару логіку
        // та було легше мігрувати навчальний проєкт.
        Director = 0,
        Teacher = 1,
        Parent = 2,
        Student = 3
    }
}
