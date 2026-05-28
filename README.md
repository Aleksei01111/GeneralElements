# Заметки для разметок

## Импорт стилей из библиотеки
```xaml
<Application.Resources>
    <ResourceDictionary>
      <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="pack://application:,,,/GeneralElementsUI;component/Styles.xaml"/>
      </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

## DataContext только для подсказок

```xaml
d:DataContext="{d:DesignInstance Type=, IsDesignTimeCreatable=True}"
```

---
мбэч
